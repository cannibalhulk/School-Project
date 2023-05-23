import React, { useRef, useEffect, useState } from 'react';
import { FaSearch } from "react-icons/fa";
import { useNavigate } from 'react-router-dom';
import { useGlobalContext } from '../../context.';
import "./SearchForm.css";
import { useDispatch } from 'react-redux';

const SearchForm = () => {
  const { setSearchTerm, setResultTitle, setFilterAuthorText, setFilterYearFrom, setFilterYearTo } = useGlobalContext();
  const searchText = useRef('');
  const filterAuthorText = useRef('');
  const filterYearFrom = useRef('');
  const filterYearTo = useRef('');

  const navigate = useNavigate();

  let [filterIsVisible, setFilterIsVisible] = useState(false);

  let filterHandler = () => {
    if (filterIsVisible) {
      setFilterIsVisible(false);
    } else {
      setFilterIsVisible(true);
    }
  }

  useEffect(() => searchText.current.focus(), []);
  const handleSubmit = (e) => {
    e.preventDefault();
    let tempSearchTerm = searchText.current.value.trim();
    if ((tempSearchTerm.replace(/[^\w\s]/gi, "")).length === 0) {
      // setSearchTerm("a");
      setResultTitle("Zəhmət olmasa birşey daxil edin...");
    } else {
      setSearchTerm(searchText.current.value);
      setFilterAuthorText(filterAuthorText.current.value);
      setFilterYearFrom(filterYearFrom.current.value);
      setFilterYearTo(filterYearTo.current.value);
    }

    navigate("/book");
  };

  return (
    <div className='search-form'>
      <div className='container'>
        <div className='search-form-content'>
          <form className='search-form' onSubmit={handleSubmit}>
            <div className='search-form-elem flex flex-sb bg-white'>
              <input type="text" className='form-control' placeholder='Kitab adı...' ref={searchText} />
              <button type="submit" className='flex flex-c' onClick={handleSubmit}>
                <FaSearch id='search-icon' size={32} />
              </button>
            </div>
            <br />
            <button id='filter' onClick={filterHandler}>Filtrlə{filterIsVisible ? <i className="fa-sharp fa-solid fa-chevron-up"></i> : <i className="fa-sharp fa-solid fa-chevron-down"></i>}</button>
            <br />
            {filterIsVisible && <div id='filteringBlock'>
              <div>
                <label>Müəllif: </label>
                <input type="text" ref={filterAuthorText} />
                <br />
                <label>Nəşr ili aralığı:</label>
                <input type="number" ref={filterYearFrom} /> <span className='range'>-dən</span>
                <input type="number" ref={filterYearTo} /> <span className='range'>-ə dək</span>
              </div>
            </div>}
          </form>
        </div>
      </div>
    </div>
  )
}

export default SearchForm