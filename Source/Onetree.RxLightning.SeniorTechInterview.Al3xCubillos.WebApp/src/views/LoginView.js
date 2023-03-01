import { useEffect } from 'react';
import * as yup from 'yup';
import { useFormik } from 'formik';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import loginService from '../services/loginService';
import { useDispatch } from "react-redux";
import { login } from "../store/user";
import { useNavigate } from "react-router-dom";
import storageHelper from '../helpers/storageHelper';
import { userDataKey } from '../store/user';

const initialValues = {
  email: 'al3x@company.com',
  password: 'test',
}

const validationSchema = yup.object({
  email: yup
    .string('Enter your email')
    .email('Enter a valid email')
    .required('Email is required'),
  password: yup
    .string('Enter your password')
    .min(3, 'Password should be of minimum 3 characters length')
    .required('Password is required'),
});

function LoginView() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    storageHelper.remove(userDataKey);
  }, []);

  const formik = useFormik({
    initialValues,
    validationSchema,
    onSubmit: async (values, { setSubmitting }) => {
      let loginResponse;

      try {        
        const { data } = await loginService.loginAsync(values);
        loginResponse = data;
      } catch (error) {
        const { response } = error;

        console.error('Login error', { 
          response,
          error,
        });

        if (response && response.status === 401) {
          alert(response.data.message);
        }
        else {
          alert('Login error. Please try again');
        }

        setSubmitting(false);
        return;
      }
      
      console.log('login response:', loginResponse);

      dispatch(
        login(loginResponse)
      );

      navigate("/patients");
    },
  });

  return (
    <Box
      sx={{
        marginTop: 10,
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
      }}
    >
      <Avatar sx={{ m: 1, bgcolor: 'warning.main' }}>
        <LockOutlinedIcon />
      </Avatar>

      <Typography component="h1" variant="h5">
        Sign in
      </Typography>

      <Box sx={{ mt: 3, mb: 5, width: '450px' }}>
        <Box component="form" onSubmit={formik.handleSubmit} noValidate>
          <TextField
            margin="normal"
            required
            fullWidth
            id="email"
            label="Email Address"
            name="email"
            autoComplete="email"
            autoFocus
            disabled={formik.isSubmitting}
            value={formik.values.email}
            onChange={formik.handleChange}
            error={formik.touched.email && Boolean(formik.errors.email)}
            helperText={formik.touched.email && formik.errors.email}
          />

          <TextField
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            autoComplete="current-password"
            disabled={formik.isSubmitting}
            value={formik.values.password}
            onChange={formik.handleChange}
            error={formik.touched.password && Boolean(formik.errors.password)}
            helperText={formik.touched.password && formik.errors.password}
          />
          
          <Button
            type="submit"
            fullWidth
            variant="contained"
            disabled={formik.isSubmitting}
            sx={{ mt: 3, mb: 0 }}
          >
            Sign In
          </Button>
        </Box>
      </Box>
    </Box>
  );
}

export default LoginView;
