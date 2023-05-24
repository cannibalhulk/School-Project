import {useState} from "react"
import {useNavigate} from "react-router-dom"
import {FaArrowLeft} from "react-icons/fa";
// import { app } from '../../fireBase/FireBase';
// import { getAuth, signInWithEmailAndPassword } from 'firebase/auth'
import "./Auth.css";
import store from '../../redux/store';

export default function Auth() {
    const navigate = useNavigate();
    let [firstname, setFirstname] = useState('');
    let [lastname, setLastname] = useState('');
    let [birthdate, setBirthdate] = useState('');
    let [phone, setPhone] = useState('');
    let [email, setEmail] = useState('');
    let [pin, setPin] = useState('');
    let [password, setPassword] = useState('');
    let [role, setRole] = useState('normal');

    let [authMode, setAuthMode] = useState("signin");
    let [token, setToken] = useState('');

    const signUp = (e) => {
        e.preventDefault(false);
        if (email && password && lastname && firstname && pin && phone && birthdate && role) {
            fetch('https://localhost:7264/api/Account/register', {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    "firstName": firstname,
                    "lastName": lastname,
                    "dateOfBirth": new Date(birthdate).toJSON(),//2004-02-12
                    "phoneNumber": phone,
                    "email": email,
                    "personalCode": pin,
                    "password": password,
                    "role": role,
                })
            })
                .then(res => res.json())
                .then(data => {
                    setFirstname('');
                    setLastname('');
                    setBirthdate('');
                    setPhone('');
                    setEmail('');
                    setPin('');
                    setPassword('');
                    setRole('normal');
                    setToken(data.token);
                    localStorage.setItem('token', token.toString());
                    store.dispatch({type: 'SIGN_IN_USER', payload: data.user});
                    navigate('/');
                }).catch((error) => {
                console.log(`Error accured: `);
                console.log(error);
            });

        } else {
            alert('Please fill in all fields.');
        }
    }
    const signIn = (e) => {
        e.preventDefault(false);
        if (email && password) {
            fetch('https://localhost:7264/api/Account/login', {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    email: email,
                    password: password
                })
            })
                .then(res => res.json())
                .then(data => {
                    setEmail('');
                    setPassword('');
                    setToken(data.token);
                    localStorage.setItem('token', token.toString());
                    store.dispatch({type: 'SIGN_IN_USER', payload: data.user});
                    navigate('/');
                }).catch((error) => {
                console.log(`Error accured: `);
                console.log(error);
            });
        }
    };

    // const handleLogout = () => {
    //     setIsAuthenticated(false);
    //     setToken('');
    // };

    const changeAuthMode = () => {
        setAuthMode(authMode === "signin" ? "signup" : "signin")
    }

    if (authMode === "signin") {
        return (
            <div className="Auth-form-container">
                <button type='button' className='back flex flex-c back-btn' onClick={() => navigate("/")}>
                    <FaArrowLeft size={22}/>
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
                                onChange={(e) => {
                                    setEmail(e.target.value)
                                }}
                            />
                        </div>
                        <div>
                            <label>Password</label>
                            <input
                                type="password"
                                className="form-control mt-1"
                                placeholder="Enter password"
                                onChange={(e) => {
                                    setPassword(e.target.value)
                                }}
                            />
                        </div>
                        <input type="submit" onClick={signIn} value='Sign In'/>

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
                <FaArrowLeft size={22}/>
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
                        <div>
                            <label>Firstname</label>
                            <input
                                type="text"
                                placeholder="Firstname"
                                onChange={(e) => {
                                    setFirstname(e.target.value)
                                }}
                            />
                        </div>
                        <div>
                            <label>Lastname</label>
                            <input
                                type="text"
                                placeholder="Lastname"
                                onChange={(e) => {
                                    setLastname(e.target.value)
                                }}
                            />
                        </div>
                        <label>Email address</label>
                        <input
                            type="email"
                            placeholder="Email Address"
                            onChange={(e) => {
                                setEmail(e.target.value)
                            }}
                        />
                    </div>
                    <div>
                        <label>Password</label>
                        <input
                            type="password"
                            placeholder="Password"
                            onChange={(e) => {
                                setPassword(e.target.value)
                            }}
                        />
                    </div>

                    <div>
                        <label>Birthdate</label>
                        <input
                            type="text"
                            placeholder="DD-MM-YYYY"
                            onChange={(e) => {
                                setBirthdate(e.target.value)
                            }}
                        />
                    </div>
                    <div>
                        <label>Personal Identification Number (FIN)</label>
                        <input
                            type="text"
                            placeholder="FIN"
                            onChange={(e) => {
                                setPin(e.target.value)
                            }}
                        />
                    </div>
                    <div>
                        <label>Phone Number</label>
                        <input
                            type="text"
                            placeholder="Phone Number"
                            onChange={(e) => {
                                setPhone(e.target.value)
                            }}
                        />
                    </div>
                    <input type="submit" className="button btn btn-primary" onClick={signUp} value='Sign Up'/>

                </div>
            </form>
        </div>
    )
}