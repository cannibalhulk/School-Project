import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { FaArrowLeft } from "react-icons/fa";
import { app } from '../../fireBase/FireBase';
import { getAuth, signInWithEmailAndPassword, createUserWithEmailAndPassword } from 'firebase/auth'
import "./Auth.css";
import store from '../../redux/store';

export default function Auth (props) {
  const navigate = useNavigate();
  let [authMode, setAuthMode] = useState("signin")
  let [email, setEmail] = useState()
  let [password, setPassword] = useState()

  const signUp = (e) => {
    e.preventDefault(false);
    if (email && password) {
      const authentication = getAuth(app);
      createUserWithEmailAndPassword(authentication, email, password)
        .then(response => {
          console.log(`Success: ${response}`);

          alert("Success!");
          setAuthMode("signin");
        })
        .catch(error => {
          console.log(`Error accured: `);
          console.log(error);
          navigate('/')
        });

    } else {
      alert('Please fill in all fields.');
    }
  }

  const signIn = (e) => {
    e.preventDefault(false);

    if (email && password) {
      const authentication = getAuth(app);
      signInWithEmailAndPassword(authentication, email, password)
        .then((userCredential) => {
          console.log(`Success!`);
          console.log(userCredential);

          localStorage.setItem('token', userCredential.user.accessToken);

          store.dispatch({ type: 'SIGN_IN_USER', payload: userCredential.user });

          navigate('/')
        })
        .catch((error) => {
          var errorCode = error.code;
          var errorMessage = error.message;
          console.log(`Error accured: `);
          console.log(error);
        });

    } else {
      alert('Please fill in all fields.');
    }


  }

  const changeAuthMode = () => {
    setAuthMode(authMode === "signin" ? "signup" : "signin")
  }

  if (authMode === "signin") {
    return (
      <div className="Auth-form-container">
        <button type='button' className='back flex flex-c back-btn' onClick={() => navigate("/")}>
        <FaArrowLeft size={22} />
        <span className='fs-18 fw-6'>Go Back</span>
      </button>
        <form className="Auth-form">
          <div className="Auth-form-content">
            <h3 className="Auth-form-title fs-24">Sign In</h3>
            <div className="text-center">
              Not registered yet?{" "}
              <a className="link" onClick={changeAuthMode}>
                Sign Up
              </a>
            </div>
            <div>
              <label>Email address</label>
              <input
                type="email"
                className="form-control mt-1"
                placeholder="Enter email"
                onChange={(e) => { setEmail(e.target.value) }}
              />
            </div>
            <div>
              <label>Password</label>
              <input
                type="password"
                className="form-control mt-1"
                placeholder="Enter password"
                onChange={(e) => { setPassword(e.target.value) }}
              />
            </div>
            <input type="submit" onClick={signIn} value='Sign In' />

            {/* <p className="text-center mt-2">
              Forgot <a href="#">password?</a>
            </p> */}
          </div>
        </form>
      </div>
    )
  }

  return (
    <div className="Auth-form-container">
      <button type='button' className='flex flex-c back-btn' onClick={() => navigate("/")}>
        <FaArrowLeft size={22} />
        <span className='fs-18 fw-6'>Go Back</span>
      </button>
      <form className="Auth-form">
        <div className="Auth-form-content">
          <h3 className="Auth-form-title fs-24">Sign Up</h3>
          <div className="text-center">
            Already registered?{" "}
            <a className="link" onClick={changeAuthMode}>
              Sign In
            </a>
          </div>
          <div>
            <label>Email address</label>
            <input
              type="email"
              placeholder="Email Address"
              onChange={(e) => { setEmail(e.target.value) }}
            />
          </div>
          <div>
            <label>Password</label>
            <input
              type="password"
              placeholder="Password"
              onChange={(e) => { setPassword(e.target.value) }}
            />
          </div>
          <input type="submit" className="button btn btn-primary" onClick={signUp} value='Sign Up' />
          {/* <p className="text-center mt-2">
            Forgot <a href="#">password?</a>
          </p> */}
        </div>
      </form>
    </div>
  )
}