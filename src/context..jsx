import React, { useState, useContext, useEffect } from 'react';
import { useCallback } from 'react';
const URL = "https://localhost:7264/api/books";
const AppContext = React.createContext();

const AppProvider = ({ children }) => {
    const [searchTerm, setSearchTerm] = useState("programming");
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [resultTitle, setResultTitle] = useState("");

    const [filterAuthorText, setFilterAuthorText] = useState("");
    const [filterYearFrom, setFilterYearFrom] = useState(0);
    const [filterYearTo, setFilterYearTo] = useState(0);

    const fetchBooks = useCallback(async () => {
        setLoading(true);
        try {
            const response = await fetch(`${URL}`);
            const data = await response.json();
             if (data) {
                let newBooks = data.slice(0, 20).filter((bookSingle) => {

                    // const pattern = new RegExp(filterAuthorText, 'i');
                    // const str = bookSingle.author_name;
                    // const isMatch = pattern.test(str);

                    // if (filterAuthorText && !isMatch) {
                    //     return false;
                    // }
                    // if (filterYearFrom && bookSingle.first_publish_year < filterYearFrom) {
                    //     return false;
                    // }
                    // if (filterYearTo && bookSingle.first_publish_year > filterYearTo) {
                    //     return false;
                    // }
                   // return true;
                }).map((bookSingle) => {
                    return {
                        id: bookSingle.id,
                        title: bookSingle.title,
                        description: bookSingle.description,
                        genre: bookSingle.genre,
                        author: bookSingle.author,
                        creationDate: bookSingle.creationDate,
                        addedDate: bookSingle.addedDate,
                        privateStatus: bookSingle.privateStatus
                    };
                });



                setBooks(newBooks);
console.log(data)
console.log(newBooks)
                if (newBooks.length > 1) {
                    setResultTitle("Axtarış nəticəsi");
                } else {
                    setResultTitle("Axtarış nəticəsi tapılmadı");
                }
            } else {
                setBooks([]);
                setResultTitle("Axtarış nəticəsi tapılmadı");
            }
            setLoading(false);
        } catch (error) {
            console.log(error);
            setLoading(false);
        }
    }, [searchTerm, filterYearFrom, filterYearTo, filterAuthorText]);

    useEffect(() => {
        fetchBooks();
    }, [searchTerm, fetchBooks]);

    return (
        <AppContext.Provider value={{
            loading, books, setSearchTerm, resultTitle, setResultTitle, setFilterAuthorText, setFilterYearFrom, setFilterYearTo
        }}>
            {children}
        </AppContext.Provider>
    )
}

export const useGlobalContext = () => {
    return useContext(AppContext);
}

export { AppContext, AppProvider };