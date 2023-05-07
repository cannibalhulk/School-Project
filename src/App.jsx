import { dark } from "@clerk/themes";
import { BrowserRouter, Routes, Route} from "react-router-dom";
import { ClerkProvider, SignIn, SignUp,SignedIn,SignedOut,RedirectToSignIn } from "@clerk/clerk-react";
import UserPage from "./components/auth/UserPage";
import HomePage from './pages/HomePage'
import './styles/App.css'
import AboutPage from "./pages/AboutPage";
import ContactPage from "./pages/ContactPage";

const clerkPubKey = import.meta.env.VITE_REACT_APP_CLERK_PUBLISHABLE_KEY;


function App() {
  
  return (
    <BrowserRouter>
      <ClerkProvider
      appearance={{
        baseTheme:dark,
        layout: {
          socialButtonsVariant: 'iconButton',
          socialButtonsPlacement: 'bottom'
        },
        variables:{
          colorBackground: "#164E63",
          fontSize: "25px"
        }
      }}
      publishableKey={clerkPubKey}
      >
        <Routes>
          <Route path="/" element={<HomePage />}/>
          <Route path="/about" element={<AboutPage />}/>
          <Route path="/contact" element={<ContactPage />}/>
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
                variables:{
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
          path="/profile"
          element={
          <>
            <SignedIn>
              <UserPage />
            </SignedIn>
             <SignedOut>
              <RedirectToSignIn/>{/*<HomePage/>*/}
            </SignedOut>
          </>
          }
        />
        </Routes>
      </ClerkProvider>
    </BrowserRouter>
  )
}

export default App
