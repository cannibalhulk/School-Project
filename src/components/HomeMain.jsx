import { useEffect, useState } from 'react';
import '../styles/HomeMain.css'
import Filter from './Filter'
import { useDispatch, useSelector } from "react-redux"
import { setSearchedBooks, clearSearchedBooks } from '../redux/slicer/bookSlicer';

function HomeMain() {

  let [searchBookInput, setSearchBookInput] = useState("");

  let bookSearch = useSelector(
    (state) => state.searchedBooksList.searchedBooks
  );

  let dispatch = useDispatch();

  useEffect(() => {
    
  }, [bookSearch]);

  let searchBookInputHandler = (event) => {
    setSearchBookInput(event.target.value);
  }

  let searchClickHandler = () => {
    dispatch(clearSearchedBooks());
    // fetch("../mydata.json", {
    //   method: "POST",
    //   headers:{
    //     "Content-type": "application/json"
    //   },
    //   body:{
    //     "searchedBook": searchBookInput
    //   }
    // })

    fetch("../mydata.json").then(data => data.json()).then(result => {
      result.data.map((single) => {
        if (single.name == searchBookInput) {
          dispatch(setSearchedBooks(single));
        }
      })
    });
  }

  return (
    <div id='home'>
      <div id='midThings'>
        <h1 id='homeText'>Kitab axtarışı</h1>
        <div id='search'>
          <input type="text" placeholder='Kitab adını daxil edin' onChange={searchBookInputHandler} />
          <input type="submit" value={"Axtar"} onClick={searchClickHandler} />
        </div>
      </div>
      <Filter />
    </div>
  )
}

export default HomeMain