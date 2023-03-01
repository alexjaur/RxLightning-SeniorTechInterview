import apiService from "./apiService";

const loginService = {
    loginAsync(loginRequest) {
        return apiService.post('/login', loginRequest);
    },
};

export default loginService;
  