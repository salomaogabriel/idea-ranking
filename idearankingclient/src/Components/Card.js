import './card.css';
import { Link } from "react-router-dom";
function Card({isLoading, title, ranking, description, id, categories, vote, isTeamOne, hideVote=false}) {
   const castVote =() =>
   {
    vote(isTeamOne);
    // TODO: Animation
   }
    if(!isLoading)
    {
        return (
            <div className="card">
            <div className='card__title'>
                <span>{ranking}</span>
                <h2>{title}</h2>
            </div>
            <div className='card__content'>
                <p>{description}</p>
                <div className='card__tags'>
                    {
                        categories.map((category, key) =>
                        {
                            return (<span key={key}>{category}</span>)

                        })
                    }
                </div>
            </div>
            <div className='card__actions'>
               {hideVote ?<></> :  <button onClick={castVote}>Vote</button>}
            <Link to={'/idea/' + id}>More</Link>
            </div>
            </div>
        );
    }
    else {
        return (
        <div className='card'>
            <div className='loading-title'></div>
            <div className='loading-content'>
            </div>
            <div className='loading__btns'>
                <div className='loading__btn'></div>
                <div className='loading__btn'></div>
            </div>
        </div>
        );
    }
}

export default Card;

