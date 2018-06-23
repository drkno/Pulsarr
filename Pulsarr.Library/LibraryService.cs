using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pulsarr.Library.ServiceInterfaces;
using Pulsarr.Metadata.ServiceInterfaces;
using Pulsarr.Preferences.DataModel.Library;
using Pulsarr.Preferences.ServiceInterfaces;

namespace Pulsarr.Library
{
    public class LibraryService : ILibraryService
    {
        private readonly IMetaDataSource _metaDataSource;
        private readonly IDatabaseManager _manager;

        public LibraryService(IMetaDataSource metaDataSource, IDatabaseManager manager)
        {
            _metaDataSource = metaDataSource;
            _manager = manager;
        }

        public IList<LibraryItem> GetAll()
        {
            return _manager.PerformDbTask(db => db.Library.ToList());
        }

        public async Task<LibraryItem> Get(ulong libraryId)
        {
            return await _manager.PerformDbTaskAsync(async db => await db.FindAsync<LibraryItem>(libraryId));
        }

        public async void Delete(ulong libraryId)
        {
            await _manager.PerformDbTaskAsync(async db =>
            {
                var libraryItem = new LibraryItem {Id = libraryId};
                db.Library.Attach(libraryItem);
                db.Library.Remove(libraryItem);
                return await db.SaveChangesAsync();
            });
        }

        public async Task<LibraryItem> RefreshMetadata(ulong libraryId)
        {
            return await _manager.PerformDbTaskAsync(async db =>
            {
                var item = await db.FindAsync<LibraryItem>(libraryId);
                var metaData = await _metaDataSource.Lookup(item.DataSourceId, item.SourceBookId);
                item.Added = DateTime.Now;
                item.State = LibraryItemState.Missing;
                item.Description = metaData.Description;
                item.DataSourceId = metaData.DataSourceId;
                item.SourceBookId = metaData.SourceBookId;
                item.Title = metaData.Title;
                item.Author = metaData.Author;
                item.ImageUrl = metaData.ImageUrl;
                item.Series = metaData.Series;
                item.SeriesPart = metaData.SeriesPart;
                item.PublicationDate = metaData.PublicationDate;
                await db.SaveChangesAsync();
                return item;
            });
        }

        public async Task<LibraryItem> Add(string dataSourceId, string bookId)
        {
            var libraryItem = new LibraryItem(dataSourceId, bookId);
            await _manager.PerformDbTaskAsync(async db =>
            {
                await db.AddAsync(libraryItem);
                return await db.SaveChangesAsync();
            });
            return await RefreshMetadata(libraryItem.Id);
        }
    }
}

