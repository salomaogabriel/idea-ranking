import './vote.css';
import { Link } from "react-router-dom";
import Card from './Card';
import { useEffect, useState } from 'react';
function Vote() {
  const [data, setData] = useState([]);
  const getQuote = async () => {
    const response = await fetch("https://localhost:7002/api/Match/vote");
    const deserializedJSON = await response.json();
    console.log(deserializedJSON);
    setData(deserializedJSON);
  }
  useEffect(() => {
    getQuote();
  },[])

  if(data.length != 0)
  {
    return (
      <div className="vote-wrapper">
  
        <Card isLoading={false} 
          title={data.ideaOne.title}
          ranking={data.ideaOne.ranking}
          description={data.ideaOne.description}
          id={data.ideaOne.id}
          categories={["Alfa",'Beta','title']}
        />
           <Card isLoading={false} 
          title={data.ideaTwo.title}
          ranking={data.ideaTwo.ranking}
          description={data.ideaTwo.description}
          id={data.ideaTwo.id}
          categories={["Alfa",'Beta','title']}
        />
        <Link to={'/all'} className='list-link'> See All</Link>
      </div>
    );
  }
  else {
    return (
      <div className="vote-wrapper">
  
        <Card isLoading={true} 
        />
           <Card isLoading={true} 
        />
        <Link to={'/all'} className='list-link'> See All</Link>
      </div>
    );
  }
 
}

export default Vote;

