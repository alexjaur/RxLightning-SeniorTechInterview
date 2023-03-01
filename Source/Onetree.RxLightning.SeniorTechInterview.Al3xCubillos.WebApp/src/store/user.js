import { createSlice } from "@reduxjs/toolkit";
import storageHelper from '../helpers/storageHelper';

export const userDataKey = "_user_";

const initialStateValue = storageHelper.get(userDataKey);
storageHelper.set(userDataKey, initialStateValue);

export const userSlice = createSlice({
  name: "user",
  initialState: { 
    value: initialStateValue,
  },
  reducers: {
    login: (state, action) => {
      state.value = action.payload;
      storageHelper.set(userDataKey, action.payload);
    },
    logout: (state) => {
      state.value = null;
      storageHelper.remove(userDataKey);
    },
  },
});

export const { login, logout } = userSlice.actions;

export default userSlice.reducer;
