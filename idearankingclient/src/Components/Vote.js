import './vote.css';
import { Link } from "react-router-dom";
import Card from './Card';
import { useEffect, useState } from 'react';
function Vote() {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const getMatch = async () => {
    setLoading(true);

    const response = await fetch("https://localhost:7002/api/Match/vote");
    const deserializedJSON = await response.json();
    setData(deserializedJSON);
    console.log(deserializedJSON)
    setLoading(false);
  }
  const vote = async (isTeamOneWinner) => {
  document.body.classList.add("body-blink");
    setTimeout(() => {
  document.body.classList.remove("body-blink");

    },200)

    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    const body = JSON.stringify({
      Id:data.id,
      IsFirstTheWinner: isTeamOneWinner
    })
    console.log(body)
    const requestOptions = {
      method: 'POST',
      mode: 'cors',
      headers: myHeaders,
      body: body,
      redirect: 'follow'
    };
    const response = await fetch("https://localhost:7002/api/Match", requestOptions)
    const deserializedJSON = await response.json();
    setData(deserializedJSON);

  }
  useEffect(() => {
 
      getMatch();
  },[])

  if(!loading)
  {
    return (
      <div className="vote-wrapper">
      <h2>Which Idea is better?</h2>
  
        <Card isLoading={false} 
          title={data.ideaOne.title}
          ranking={data.ideaOne.ranking}
          description={data.ideaOne.description}
          id={data.ideaOne.id}
          categories={data.ideaOne.categories}
          vote={vote}
          isTeamOne= {true}
        />
           <Card isLoading={false} 
          title={data.ideaTwo.title}
          ranking={data.ideaTwo.ranking}
          description={data.ideaTwo.description}
          id={data.ideaTwo.id}
          categories={data.ideaOne.categories}
          vote={vote}
          isTeamOne= {false}


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

