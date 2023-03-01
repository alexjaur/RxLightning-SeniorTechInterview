import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import './App.css';

import {
  BrowserRouter,
  Navigate,
  Routes,
  Route,
} from "react-router-dom";

import Container from '@mui/material/Container';
import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import { createTheme, ThemeProvider } from '@mui/material/styles';

import LoginView from './views/LoginView';
import PatientsView from './views/PatientsView';

const theme = createTheme();

function App() {
  return (
    <ThemeProvider theme={theme}>
      <Container component="div" maxWidth="lg" sx={{ my: 0 }}>
        <CssBaseline />

        <BrowserRouter>
          <div className="App">
            <>
              <Routes>
                <Route path='/login' element={<LoginView />} />
                <Route path='/patients' element={<PatientsView />} />
                <Route path='*' element={<Navigate to='/login' />} />
              </Routes>
            </>
          </div>
        </BrowserRouter>
        <Box
          sx={{
            mt: 5,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Typography variant="overline" color="text.secondary" align="center">
            Senior Tech Interview
          </Typography>
          
          <Typography variant="body2" color="text.primary" align="center">
            Al3x Cubillos (onetree)
          </Typography>
        </Box>
      </Container>
    </ThemeProvider>
  );
}

export default App;
