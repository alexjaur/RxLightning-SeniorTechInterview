import { useState, useEffect, forwardRef } from 'react';
import { useSelector, useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { DataGrid } from '@mui/x-data-grid';
import Avatar from '@mui/material/Avatar';
import Box from '@mui/material/Box';
import ListAltOutlinedIcon from '@mui/icons-material/ListAltOutlined';
import Typography from '@mui/material/Typography';
import Dialog from '@mui/material/Dialog';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';
import Slide from '@mui/material/Slide';
import Grid from '@mui/material/Grid';
import patientsService from '../services/patientsService';
import { logout } from "../store/user";

const Transition = forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

function getPatientNormalized(p) {
  const patientItem = {
    ...p,
    id: p.patientId,
    fullName: `${p.firstName || ''} ${p.lastName || ''}`,
  };

  return patientItem;
}

const columns = [
  { field: 'id', hide: true },
  {
    field: 'fullName',
    headerName: 'Full name',
    width: 180,
  },
  {
    field: 'fullAddress',
    headerName: 'Address',
    sortable: false,
    width: 260,
    valueGetter: (params) =>
      `${params.row.addressLine1 || ''}, ${params.row.addressLine2 || ''}`,
  },
  {
    field: 'fullCity',
    headerName: 'City (State)',
    sortable: false,
    width: 180,
    valueGetter: (params) =>
      `${params.row.city || ''} (${params.row.state || ''})`,
  },
  { field: 'postalCode', headerName: 'Postal Code', width: 120 },
  { field: 'gender', headerName: 'Gender', width: 120 },
  { field: 'dateOfBirth', headerName: 'Date Of Birth', width: 150 },
];

function PatientsView() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  
  const user = useSelector((state) => state.user.value);
  
  const [open, setOpen] = useState(false);
  const [patient, setPatient] = useState(null);
  const [patients, setPatients] = useState([]);
  const [selectionModel, setSelectionModel] = useState([]);

  const handleClose = () => {
    setOpen(false);
  };
  
  const handleClickLogout = () => {    
    dispatch(
      logout()
    );
    navigate("/login");
  };
  

  useEffect(() => {
    if (!user || !user.accessToken) {
      navigate("/login");
      return;
    }

    (async () => {

      try {        
        const { data } = await patientsService.getAllAsync();
        
        const parsedData = data.map((p) => {
          const patientItem = getPatientNormalized(p);
          return patientItem;
        });

        setPatients(parsedData);
      } catch (error) {
        const { response } = error;
  
        console.error('Patients error', { 
          response,
          error,
        });
  
        if (response && response.status === 401) {
          navigate("/login");
        }
        else {
          alert('Patients error. Please try again');
        }
      }
    })();
    return () => {};
  }, [user, navigate]);

  return (
    <Box
      sx={{
        marginTop: 8,
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
      }}
    >
      <Box
        sx={{
          width: '100%',
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'flex-end',
        }}
      >
        <Button
          color="error"
          sx={{ mt: 1, mb: 2 }}
          onClick={handleClickLogout}
        >
          Logout
        </Button>
      </Box>

      <Avatar sx={{ m: 1, bgcolor: 'success.main' }}>
        <ListAltOutlinedIcon />
      </Avatar>

      <Typography component="h1" variant="h5" sx={{ mb: 4 }}>
        Patient List
      </Typography>

      <div style={{ width: '100%' }}>
        <DataGrid
          autoHeight
          keepNonExistentRowsSelected 
          rows={patients}
          columns={columns}
          pageSize={10}
          rowsPerPageOptions={[10]}
          onSelectionModelChange={async (newSelectionModel) => {
            console.log('newSelectionModel', newSelectionModel)
            setSelectionModel(newSelectionModel);
            const [id] = newSelectionModel;
            const { data } = await patientsService.getByIdAsync(id);
            const patientItem = getPatientNormalized(data);
            setPatient(patientItem);
            setOpen(true);
          }}
          selectionModel={selectionModel}
        />
      </div>

      <Dialog
        fullScreen
        open={open}
        onClose={handleClose}
        TransitionComponent={Transition}
      >
        <AppBar color='success' sx={{ position: 'relative' }}>
          <Toolbar>
            <IconButton
              edge="start"
              color="inherit"
              onClick={handleClose}
              aria-label="close"
            >
              <CloseIcon />
            </IconButton>
            <Typography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
              Patient Details
            </Typography>
          </Toolbar>
        </AppBar>
        <Box 
          sx={{
            margin: 0,
            padding: 5,
            width: '100%',
          }}
        >
          {(patient)
            ? <>
                <Box sx={{ flexGrow: 1, margin: '0 auto' }}>
                  <Grid container spacing={2}>
                    <Grid item>
                      {(patient.gender === 'Male')
                        ? <Avatar 
                            alt={ patient.fullName } 
                            sx={{ width: 80, height: 80, mr: 2 }}
                            src="/static/avatar-man.png" />
                        : <Avatar 
                          alt={ patient.fullName } 
                          sx={{ width: 80, height: 80, mr: 2 }}
                          src="/static/avatar-woman.png" />
                      }
                    </Grid>
                    <Grid item xs={8}>
                      <Typography component="div" variant="h3">
                        { patient.fullName } 
                      </Typography>
                      
                      <Typography component="div" variant="overline">
                        was born on { patient.dateOfBirth || ''}
                      </Typography>                  
                    </Grid>
                    <Grid item xs={12} pt={2}>
                      <div className='patient-card'>
                        <Typography component="div" variant="subtitle1">
                          { patient.addressLine1 || ''}, { patient.addressLine2 } - { patient.city || ''} ({ patient.state })
                        </Typography>
            
                        <Typography component="div" variant="body2">
                          Postal Code: { patient.postalCode || ''}
                        </Typography>
                      </div>
                    </Grid>
                  </Grid>
                </Box>
              </>
            : <> </>
          }
        </Box>
        
      </Dialog>
    </Box>
  );
}

export default PatientsView;
