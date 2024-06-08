
import { Link } from 'react-router-dom';

const Sidebar = () => {
  return (
    <div className="sidebar">
      <ul>
        <li>
          <Link to="#">Staff</Link>
        </li>
        <li>
          <Link to="#">Devices</Link>
        </li>
      </ul>
    </div>
  );
};

export default Sidebar;