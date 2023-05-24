// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
 
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration

// require("dotenv").config();
// apiKey: process.env.apiKey,
// authDomain:  process.env.authDomain,
// projectId:  process.env.projectId,
// storageBucket:  process.env.storageBucket,
// messagingSenderId:  process.env.messagingSenderId,
// appId:  process.env.appId
 
const firebaseConfig = {
  apiKey: "AIzaSyBpMqVuOsY8iUSmtKXtbd1qK4xoM272kCk",
  authDomain: "bookcenter-9f2e6.firebaseapp.com",
  projectId: "bookcenter-9f2e6",
  storageBucket: "bookcenter-9f2e6.appspot.com",
  messagingSenderId: "105130139396",
  appId: "1:105130139396:web:2cb3f1f47ff5b05469ba51"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);

export {app}; 