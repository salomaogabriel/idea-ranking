import './idea.css';
import { useParams } from "react-router-dom";

function Idea() {
  let { id } = useParams();
  return (
    <header className="main-header">
        Idea
    </header>
  );
}

export default Idea;

