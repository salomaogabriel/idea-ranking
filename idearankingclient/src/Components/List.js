import './list.css';
import { Link } from "react-router-dom";
import { useState, useEffect } from 'react';
import Card from './Card';
function List() {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(0);
  const getData = async () => {
    const response = await fetch("https://localhost:7002/api/Ideas/page/" + page);
    const deserializedJSON = await response.json();
    setData(deserializedJSON["$values"]);
    console.log(deserializedJSON["$values"])
  }
  useEffect(() => {
    getData();
  },[])
  useEffect(() => {
    console.log(page)
    if(page < 0) page = 0;
    getData();
  },[page])
  if(data.length != 0)
  {
    return (
      <div className="list-all">
          {data.map((item) =>
          {
            return (
              <Card isLoading={false} 
          title={item.title}
          ranking={item.ranking}
          id={item.id}
          description={item.description}
          categories={["Alfa",'Beta','title']}
          hideVote={true}


        />
            );
          })
          }
          <div className='pagination'>
            <button className={'pagination--prev ' + (page != 0 ? '' : 'disabled')} onClick={() => setPage(page -1)}>Previous</button>
            <button className='pagination--next' onClick={() => setPage(page + 1)}>Next</button>
          </div>
      </div>
    );
  }
  return (
    <div className="list-all">
       <Card isLoading={true} 
        />
        <Card isLoading={true} 
        />
        <Card isLoading={true} 
        />
        <Card isLoading={true} 
        />
        <Card isLoading={true} 
        />
    </div>
  );
}

export default List;

