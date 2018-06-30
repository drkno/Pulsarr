import ActivityRoutes from './activity';
import LibraryRoutes from './library';
import NewRoutes from './new';
import SettingsRoutes from './settings';
import LoginRoutes from './login';

export default ActivityRoutes.concat(LibraryRoutes, NewRoutes, SettingsRoutes, LoginRoutes);
