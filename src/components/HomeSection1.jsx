import { useEffect, useState } from 'react'
import '../styles/HomeSection1.css'
import { useSelector } from 'react-redux';

function HomeSection1() {

  let [booksVisible, setBooksVisible] = useState(false);

  let searchResult = useSelector((state) => state.searchedBooksList.searchedBooks);

  useEffect(() => {
    console.log("section: ", typeof searchResult);
  }, [searchResult]);

  return (
    <div id='books'>
      <div id='grid-container'>
        {
          searchResult.map((single) => {
            return <div className='grid-item'>
              <h1>{single.name}</h1>
              <p>{single.author}</p>
              <p>{single.year}</p>
            </div>
          })
        }
      </div>
    </div>
  )
}

export default HomeSection1
