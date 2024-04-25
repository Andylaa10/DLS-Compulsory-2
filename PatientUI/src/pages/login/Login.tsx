import {Box, Card, CssBaseline, Typography, TextField, Button} from "@mui/material";
import './Login.css'
import React, { useState } from 'react';
import PatientLogo from '../assets/patient.png';

export const Login: React.FunctionComponent = () => {
  const [ssn, setSsn] = useState('');

  const handleLogin = () => {
    // Handle login logic here
    console.log(ssn);
  };

  return (
      <>
        <CssBaseline/>
        <Box className="container" sx={{backgroundColor: "#2e3031"}}>
          <Card sx={{backgroundColor: "#cecaca", width: 350, height: 450, display: 'flex', flexDirection: 'column', justifyContent: 'center', alignItems: 'center'}}>
            <Typography variant="h4" gutterBottom>
              <img style={{width: 175}} src={PatientLogo} alt={"patients"}/>
            </Typography>
            <TextField
                variant="outlined"
                margin="normal"
                required
                id="ssn"
                label="SSN"
                name="ssn"
                autoComplete="ssn"
                autoFocus
                sx={{width: 250}}
                value={ssn}
                onChange={(e) => setSsn(e.target.value)}
            />
            <Button
                type="submit"
                sx={{width: 250}}
                variant="contained"
                color="primary"
                onClick={handleLogin}
            >
              Login
            </Button>
          </Card>
        </Box>
      </>
  );
}