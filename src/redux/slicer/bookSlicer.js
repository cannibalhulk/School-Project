import { createSlice } from '@reduxjs/toolkit';

let initialState = {
  searchedBooks: []
}

export const searchedBooksSlice = createSlice({
  name: "searchedBooks",
  initialState,
  reducers: {
    setSearchedBooks: (state, action) => {
      state.searchedBooks = [...state.searchedBooks, action.payload]
    },
    clearSearchedBooks: (state, action) => {
      state.searchedBooks = []
    }
  },
}
);

export const { setSearchedBooks, clearSearchedBooks } = searchedBooksSlice.actions

export default searchedBooksSlice.reducer