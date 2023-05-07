// import { createStore } from "redux";
// import { reducer } from "./reducer";

// const store = createStore(cartReducer);

// export default store;

import { configureStore } from '@reduxjs/toolkit'
import bookSlicerReducer from "./slicer/bookSlicer"

export default configureStore({
  reducer: {
    searchedBooksList: bookSlicerReducer
  },
});