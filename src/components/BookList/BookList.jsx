import React, { useEffect,useState } from 'react';
import { useGlobalContext } from '../../context.';
import Book from "./Book";
import Loading from "../Loader/Loader";
import coverImg from "../../assets/images/cover_not_found.jpg";
import "./BookList.css";
import { PaginationControl } from 'react-bootstrap-pagination-control';
//https://covers.openlibrary.org/b/id/240727-S.jpg

const BookList = () => {
  const { books, loading, resultTitle } = useGlobalContext();
  const [page, setPage] = useState(1);


    const booksWithCovers = books.map((book) => {
         return {
            ...book,
            photoData: (/(sjpg|gif|png|JPG|GIF|PNG|JPEG|jpeg)$/.test(book.photoData))  ? book.photoData : coverImg,
        }
    });  if (loading) return <Loading />;

  return (
    <section className='booklist'>
      <div className='container'>
        <div className='section-title'>
          <h2>{resultTitle}</h2>
        </div>
        <div className='booklist-content grid'>
          {
            booksWithCovers.slice(0, 30).map((item, index) => {
              return (
                <Book {...item} key={index}/>
              )
            })
          }
        </div>
        <div className="pageContainer">
          <PaginationControl
              page={page}
              between={4}
              total={250}
              limit={20}
              changePage={(page) => {
                setPage(page);
                console.log(page)
              }}
              ellipsis={1}
          />
        </div>
      </div>

    </section>
  )
}

export default BookList