import React, {useState, useEffect} from 'react';
import {useParams} from 'react-router-dom';
import Loading from "../Loader/Loader";
import coverImg from "../../assets/images/cover_not_found.jpg";
import "./BookDetails.css";
import {FaArrowLeft, FaLock, FaLockOpen} from "react-icons/fa";
import {useNavigate} from 'react-router-dom';
import {Typography, Rating} from '@mui/material'

const URL = "https://localhost:7264/api/books";
import {MDBBtn, MDBIcon} from 'mdb-react-ui-kit';

const BookDetails = () => {
    const {id} = useParams();
    const [loading, setLoading] = useState(false);
    const [book, setBook] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        setLoading(true);

        async function getBookDetails() {
            try {
                const response = await fetch(`${URL}/${id}`);
                const data = await response.json();
                console.log(data);

                if (data) {
                    const {
                        id,
                        title,
                        description,
                        genre,
                        author,
                        creationDate,
                        addedDate,
                        privateStatus,
                        fileData,
                        photoData
                    } = data;

                    const newBook = {
                        id: id,
                        photoData: (/(sjpg|gif|png|JPG|GIF|PNG|JPEG|jpeg)$/.test(photoData)) ? photoData : coverImg,
                        title: title ? title : "Məzmun tapılmadı",
                        author: author ? author : "Məzmun tapılmadı",
                        description: description ? description : "Məzmun tapılmadı",
                        genre: genre ? genre : "Məzmun tapılmadı",
                        creationDate: creationDate ? creationDate : "Məzmun tapılmadı",
                        addedDate: addedDate ? addedDate : "Məzmun tapılmadı",
                        privateStatus: privateStatus,
                        fileData: privateStatus ? null : fileData
                    };
                    setBook(newBook);
                } else {
                    setBook(null);
                }
                setLoading(false);
            } catch (error) {
                console.log(error);
                setLoading(false);
            }
        }

        getBookDetails();
    }, [id]);
// bag
    const dowloadFile = (e) => {
        e.preventDefault();

        console.log("a");
        if (!book.privateStatus) {
          const element = document.createElement("a");
          const file = new Blob([book.fileData], {type: 'text/plain'});
          element.href = URL.createObjectURL(file);
          element.download = `${book.title}.json`;
          document.body.appendChild(element);
          element.click();

        }
    }

    if (loading) return <Loading/>;

    return (
        <section className='book-details'>
            <div className='container'>
                <button type='button' className='flex flex-c back-btn' onClick={() => navigate("/book")}>
                    <FaArrowLeft size={22}/>
                    <span className='fs-18 fw-6'>Geri qayıt</span>
                </button>
                <div class="container">
                    <Typography className='fs-26' component="legend">{/*Read only*/}</Typography>
                    <Rating className='fs-26' name="read-only" value={4} readOnly/>
                </div>
                <div className='book-details-content grid'>
                    <div className='book-details-img'>
                        <img src={book?.photoData} alt="cover img"/>
                    </div>
                    <div className='book-details-info'>
                        <div className='book-details-item title'>
                            <span className='fw-6 fs-24'>{book?.title} {book?.privateStatus ? <FaLock/> :
                                <FaLockOpen/>}</span>


                        </div>
                        <div className='book-details-item description'>
                            <span>{book?.description}</span>
                        </div>
                        <div className='book-details-item'>
                            <span className='fw-6'>Janr: </span>
                            <span className='text-italic'>{book?.genre}</span>
                        </div>
                        <div className='book-details-item'>
                            <span className='fw-6'>Müəllif: </span>
                            <span>{book?.author}</span>
                        </div>

                        <div className='book-details-item'>
                            <span className='fw-6'>Yaradılma tarixi:   </span>
                            <span
                                className='text-italic'>{book?.creationDate !== "Məzmun tapılmadı" ? new Date(book?.creationDate).toLocaleDateString() : book?.creationDate}</span>
                        </div>
                        <div className='book-details-item'>
                            <span className='fw-6'>Yüklənmiş vaxtlar:  </span>
                            <span
                                className='text-italic'>{book?.addedDate !== "Məzmun tapılmadı" ? new Date(book?.addedDate).toLocaleDateString() : book?.addedDate}</span>
                        </div>

                        {book?.privateStatus ? null : <MDBBtn floating tag='a' onClick={dowloadFile}>
                            <MDBIcon className="fs-26" fas icon='download'/>
                        </MDBBtn>}

                    </div>
                </div>
            </div>
        </section>
    )
}

export default BookDetails