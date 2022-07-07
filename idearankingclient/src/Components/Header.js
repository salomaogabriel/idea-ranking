// import './App.css';
import './header.css';
import { Link } from "react-router-dom";
function Header({LinkToCreate}) {
  return (
    <header className="main-header">
        <h2>Ideas Ranking</h2>
        <Link to={LinkToCreate ?  "/Create" : "/"} className="main-header__btn">
         {LinkToCreate ? <>Add Idea</> :<>Go Vote</>}
        </Link>
         
    </header>
  );
}

export default Header;

