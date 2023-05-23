import React from 'react';
import { Link } from 'react-router-dom';
import store from '../../redux/store'
import { useSelector } from 'react-redux'
import "./BookList.css";

const Book = (book) => {
  let favorites = useSelector((state) => state.favorites);
  const currentUser = useSelector((state) => state.user);

  const add = () => {
    store.dispatch({ type: 'ADD_TO_FAVORITE', payload: book });
  }

  const remove = () => {
    store.dispatch({ type: 'REMOVE_FROM_FAVORITE', payload: book });
  }

  const exist = () => {
    return !!favorites.find((el) => el.id == book.id);
  }
  return (
    <div className='book-item flex flex-column flex-sb'>
      {
        currentUser?.email &&
        <div className='book-item'>
          {
            exist() ?
              <input type='button' onClick={remove} className='fs-16' value='delete from Favorite' /> :
              <input type='button' onClick={add} className='fs-16' value='add to Favorite' />
          }
        </div>
      }
      <div className='book-item-img'>
        <img src={book.cover_img} alt="cover" />
      </div>
      <div className='book-item-info text-center'>
        <Link to={`/book/${book.id}`} {...book}>
          <div className='book-item-info-item title fw-7 fs-18'>
            <span>{book.title}</span>
          </div>
        </Link>

        <div className='book-item-info-item author fs-15'>
          <span className='text-capitalize fw-7'>Müəllif: </span>
          <span>{book.author}</span>
        </div>

        <div className='book-item-info-item edition-count fs-15'>
          <span className='text-capitalize fw-7'>Ümumi nəşrlər: </span>
          <span>{book.edition_count}</span>
        </div>

        <div className='book-item-info-item publish-year fs-15'>
          <span className='text-capitalize fw-7'>İlk nəşr ili: </span>
          <span>{book.first_publish_year}</span>
        </div>
      </div>
    </div>
  )
}

export default Book