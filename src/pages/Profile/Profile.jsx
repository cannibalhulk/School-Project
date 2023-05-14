import React from 'react';
import { useSelector } from 'react-redux'
import store from '../../redux/store';
import { useNavigate } from "react-router-dom"
import profileImg from "../../images/user-img.jpg";
import Book from "../../components/BookList/Book.jsx"
import "./Profile.css";


const Profile = () => {
    const navigate = useNavigate();
    const currentUser = useSelector((state) => state.user);
    let favorites = useSelector((state) => state.favorites);

//    const auth = getAuth(app);
    // let [user, setUser] = useState();

    // useEffect(() => {
    //     onAuthStateChanged(auth, (u) => {
    //         if (u) {
    //             console.log(u);
    //             setUser(u);
    //         } else {
    //             alert('nooooo!');
    //             setUser({});
    //             return navigate('/');
    //         }
    //     });
    // })

    const exit = () => {
        signOut(auth).then(() => {
            alert('Bye! 😭');
            store.dispatch({ type: 'SIGN_IN_USER', payload: {} });

            return navigate('/');
        }).catch((error) => {
            console.log(`Error accured: `);
            console.log(error);
            return navigate('/');
        });
    }

    return (
        <section className='profile'>
            <div className='container'>
                <div className='section-title'>
                    <h2>Profile</h2>
                </div>

                <div className='profile-content grid'>
                    <div className='profile-img'>
                        <img src={profileImg} alt="" />
                    </div>
                    <div className='profile-text'>
                        <h2 className='profile-title fs-26 ls-1'>{currentUser?.email}</h2>
                    </div>
                    <div className='profile-text'> 
                    <input type='button' onClick={exit} to="profile" className='profile-exit fs-22 fw-6 ls-1' value='Exit' />
                    </div>
                </div>
            </div>
            <div className='container'>
                <div className='booklist-content grid'>
                    {
                        favorites.map((item, index) => {
                            return (
                                <Book key={index} {...item} />
                            )
                        })
                    }
                </div>
            </div>
        </section>
    )
}

export default Profile
