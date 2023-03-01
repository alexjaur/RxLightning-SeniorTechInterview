import apiService from "./apiService";

const patientsService = {
    getAllAsync() {
        return apiService.get('/patients');
    },
    getByIdAsync(id) {
        return apiService.get(`/patients/${id}`);
    },
};

export default patientsService;
