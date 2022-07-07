import './list.css';
import { Link } from "react-router-dom";
import { useState, useEffect } from 'react';
import Card from './Card';
function List() {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(0);
  const [maxPage, setMaxPage] = useState(0);
  const [loading, setLoading] = useState(true);

  const getData = async () => {
    setLoading(true)
    const response = await fetch("https://localhost:7002/api/Ideas/page/" + page);
    const deserializedJSON = await response.json();
    setData(deserializedJSON.ideas["$values"]);
    setMaxPage(deserializedJSON.maxPage)
    setLoading(false);
  }
  useEffect(() => {
    getData();
  },[])
  useEffect(() => {
    console.log(page)
    if(page < 0)
    {
      setPage(0);
      return;
    } 
      
    if(page > maxPage - 1) 
    {

      setPage(maxPage -1);
      return;
    }
    getData();
  },[page])
  if(!loading)
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
          categories={item.categories}
          hideVote={true}


        />
            );
          })
          }
          <div className='pagination'>
            <button className={'pagination--prev ' + (page != 0 ? '' : 'disabled')} onClick={() => setPage(page -1)}>Previous</button>
            <button className={'pagination--prev ' + (page < maxPage -1 ? '' : 'disabled')} onClick={() => setPage(page + 1)}>Next</button>
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

