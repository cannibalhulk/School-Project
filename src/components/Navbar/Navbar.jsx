import React, { useState } from 'react';
import { useSelector } from 'react-redux'
import { Link } from 'react-router-dom';
import "./Navbar.css";
import { HiOutlineMenuAlt3 } from "react-icons/hi";
import logoImg from "../../assets/images/bookLogo.png"

const Navbar = () => {
  const [toggleMenu, setToggleMenu] = useState(false);
  const handleNavbar = () => setToggleMenu(!toggleMenu);
  const currentUser = useSelector((state) => state.user);
  // let [user, setUser] = useState();
  // const auth = getAuth(app);

  // useEffect(() => {
  //   onAuthStateChanged(auth, (u) => {
  //     if (u) {
  //       setUser(u); 
  //     } 
  //     else{
  //       setUser({});
  //     } 
  //   });
  // });

  return (
    <nav className='navbar' id="navbar">
      <div className='container navbar-content flex'>
        <div className='brand-and-toggler flex flex-sb'>
          <Link to="/" className='navbar-brand flex'>
            <img src={logoImg} alt="site logo" />
            <span className='text-uppercase fw-7 fs-24 ls-1'>Kitabmərkəzi</span>
          </Link>
          <button type="button" className='navbar-toggler-btn' onClick={handleNavbar}>
            <HiOutlineMenuAlt3 size={35} style={{
              color: `${!toggleMenu ? "rgb(239, 208, 36)" : "#fff"}`
            }} />
          </button>
        </div>

        <div className={toggleMenu ? "navbar-collapse show-navbar-collapse" : "navbar-collapse"}>
          <ul className="navbar-nav">
            <li className='nav-item'>
              <Link to="book" className='nav-link text-uppercase text-white fs-22 fw-6 ls-1'>Ana səhifə</Link>
            </li>
            <li className='nav-item'>
              {
                currentUser?.email ? <li className='nav-item'>
                  <Link to="profile" className='nav-link text-uppercase text-white fs-22 fw-6 ls-1'>Profile</Link>
                </li> : <Link to="login" className='nav-link text-uppercase text-white fs-22 fw-6 ls-1'>Daxil ol</Link>
              }
            </li>
            <li className='nav-item'>
              <Link to="about" className='nav-link text-uppercase text-white fs-22 fw-6 ls-1'>Haqqımızda</Link>
            </li>
            <li className='nav-item'>
              <Link to="contact" className='nav-link text-uppercase text-white fs-22 fw-6 ls-1'>Əlaqə</Link>
            </li>

          </ul>
        </div>
      </div>
    </nav>
  )
}

export default Navbar