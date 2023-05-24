import React from 'react';
import ReactDOM from 'react-dom/client';
import {
  BrowserRouter, Routes, Route
} from 'react-router-dom';
import { AppProvider } from './context.';
import './index.css';
import Home from './pages/Home/Home';
import About from "./pages/About/About";
import Contact from "./pages/Contact/Contact";
import Profile from "./pages/Profile/Profile";
import BookList from "./components/BookList/BookList";
import BookDetails from "./components/BookDetails/BookDetails";
import { Provider } from 'react-redux'
import store from './redux/store'
import { ClerkProvider, SignIn, SignUp, SignedIn, SignedOut, RedirectToSignIn } from "@clerk/clerk-react";
import { dark } from '@clerk/themes';
import UserPage from "./components/Auth/UserPage";

const clerkPubKey = import.meta.env.VITE_REACT_APP_CLERK_PUBLISHABLE_KEY;

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <Provider store={store}>
    <AppProvider>
      <BrowserRouter>
        <ClerkProvider
          appearance={{
            baseTheme: dark,
            layout: {
              socialButtonsVariant: 'iconButton',
              socialButtonsPlacement: 'bottom'
            },
            variables: {
              colorBackground: "#164E63",
              fontSize: "25px"
            }
          }}
          publishableKey={clerkPubKey}
        >
          <Routes>
            <Route path="/" element={<Home />}>
              <Route path="about" element={<About />} />
              <Route path="contact" element={<Contact />} />
              <Route path="book" element={<BookList />} />
              <Route path="/book/:id" element={<BookDetails />} />

            </Route>
            <Route
              path="/login/*"
              element={
                <SignIn
                  routing="path"
                  path="/login"
                  appearance={{
                    layout: {
                      socialButtonsVariant: 'iconButton',
                      socialButtonsPlacement: 'bottom'
                    },
                    variables: {
                      colorBackground: "#164E63",
                      fontSize: "25px"
                    }
                  }}
                />
              }
            />
            <Route
              path="/sign-up/*"
              element={<SignUp routing="path" path="/register" />}
            />
            <Route
              path="/login"
              element={
                <>
                  <SignedIn>
                    <UserPage />
                  </SignedIn>
                  <SignedOut>
                    {/* <RedirectToSignIn /> */}
                    <Home />
                  </SignedOut>
                </>
              }
            />
          </Routes>
        </ClerkProvider>
      </BrowserRouter>
    </AppProvider>
  </Provider>
); 