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
import Auth from "./pages/Auth/Auth";
import BookDetails from "./components/BookDetails/BookDetails";
import { Provider } from 'react-redux'
import store from './redux/store'

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <Provider store={store}>
    <AppProvider>
      <BrowserRouter>
          <Routes>
            <Route path="/" element={<Home />}>
              <Route path="about" element={<About />} />
              <Route path="contact" element={<Contact />} />
              <Route path="book" element={<BookList />} />
              <Route path="/book/:id" element={<BookDetails />} />
              <Route path="/profile" element={<Profile />} />
            </Route>
            <Route path="auth" element={<Auth />} />
          </Routes>
      </BrowserRouter>
    </AppProvider>
  </Provider>
); 