import axios from "axios";
import storageHelper from '../helpers/storageHelper';
import { userDataKey } from '../store/user';

const baseApiURL = process.env.REACT_APP_BASE_API_URL || "";

if (!baseApiURL) {
  console.warn('`baseApiURL` is not definded');
}
else {
  console.log('`baseApiURL` is defined and its value is:', baseApiURL);
}

const apiService = axios.create({ 
  baseURL: baseApiURL,
  headers: {
    "Content-Type": "application/json",
  },
});

apiService.interceptors.request.use(
  (config) => {
    const user = storageHelper.get(userDataKey);
    const token = user ? user.accessToken : null;

    if (token) {
      config.headers.authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => Promise.reject(error)
);

apiService.interceptors.response.use(
  (response) => response,
  (error) => {
    return Promise.reject(error);
  }
);
  
export default apiService;
  