import './idea.css';
import { useParams } from "react-router-dom";
import { useEffect, useState } from 'react';
import Chart from './Chart';

function Idea() {
  let { id } = useParams();

  const [data, setData] = useState([]);
  const [labels, setLabels] = useState([]);
  const [historyData, setHistoryData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [last10, setLast10] = useState([1,1,1,1,1,1,1,1,1,1])
  const getData = async () => {
    setLoading(true);

    const response = await fetch("https://idearanking.azurewebsites.net/api/ideas/" + id);
    const deserializedJSON = await response.json();
    setData(deserializedJSON);
    let tempLabels = [];
    let tempHistory = [];
    let statuses = [];
    deserializedJSON.history["$values"].map((value, index) => {
      tempLabels.push(index);
      tempHistory.push(value.ranking);
      statuses.push(value.status)
    });
    setLabels(tempLabels);
    setHistoryData(tempHistory);

    if(statuses.length > 9)
      setLast10(statuses.reverse().slice(0, 10))
    else {
      statuses.reverse().push(...last10);
      setLast10(statuses.slice(0,10));
      }
    setLoading(false);
  }
  useEffect(() => {
    getData();
  }, []);
  
  
  if(!loading)
  {
    return (
      <div className="idea-screen">
          <div className='idea__header'>
            <span>{data.ranking}</span>
            <h2>{data.title}</h2>
          </div>
          <p className='idea-screen__content'>{data.description}</p>
          <div className='idea__tags'>
            {
                data.categories["$values"].map((category, key) =>
                {
                    return (<span key={key}>{category.name}</span>)

                })
            }
          </div>
          <div className='idea__stats'>
            <p>Voting : <span className='stat'>{data.numberOfMatches}</span></p>
            <p>Wins: <span className='stat'>{data.wins}</span></p>
            <p>Highest Rating: <span className='stat'>{data.biggestRating}</span></p>
            
            <div className='chart'>
            <Chart labels={labels}
            data={historyData}
            maxRating={data.biggestRating}/>
            </div>
            <p>Last 10:</p>
            <div className='last-10'>

             {last10.map((item, key) => {
              if(item == 0)
                return (<span key={key} className='won'></span>);
              if(item == 1)
                return (<span key={key}></span>);
              if(item == 2)
                return (<span key={key} className='lost'></span>);
              return (<></>);
             })}
            </div>
          </div>
          
      </div>
    );
  }
  return (
    <div className="idea-screen">
        Loading
    </div>
  );
  
}

export default Idea;

