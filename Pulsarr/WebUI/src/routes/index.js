import ActivityRoutes from './activity';
import LibraryRoutes from './library';
import NewRoutes from './new';
import SettingsRoutes from './settings';

export default ActivityRoutes.concat(LibraryRoutes, NewRoutes, SettingsRoutes);
