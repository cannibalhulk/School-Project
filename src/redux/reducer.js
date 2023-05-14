let initialState =
{
    user: {},
    token: localStorage.getItem('token'),
    favorites: [],
    filterIsVisible: false
}


function reducer(state = initialState, action) {
    switch (action.type) {
        case "SIGN_IN_USER":
            {
                return { ...state, user: action.payload };
            }
        case "ADD_TO_FAVORITE":
            {
                if (!state.favorites.find((el) => el.id === action.payload.id)) {
                    return { ...state, favorites: [...state.favorites, action.payload] };
                }
                break;
            }
        case "REMOVE_FROM_FAVORITE":
            {
                let copy = state.favorites.filter((el) => el.id !== action.payload.id);
                return { ...state, favorites: copy };
            }
        default:
            return state;
    }
}

export default reducer;