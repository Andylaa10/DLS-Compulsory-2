import {
  Box,
  Card,
  CssBaseline,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Typography,
} from '@mui/material'
import './Home.css'
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import CancelIcon from '@mui/icons-material/Cancel';
import * as React from "react";

export const Home: React.FunctionComponent = () => {

  return (
      <>
        <CssBaseline/>
        <Box className="container" sx={{backgroundColor: "#2e3031"}}>
          <Card sx={{width: "80vw", height: "90vh", backgroundColor: "#bfc9dc"}}>
            <Card sx={{backgroundColor: "#415967", width: "100%", padding: 1}}>
              <Box display="flex" justifyContent="space-between">
                <Typography paddingLeft={2} variant="h3" color={"white"}>
                  PatientUI
                </Typography>
                <Typography sx={{color: "#fff"}}>
                  NAME OF LOGGED IN PATIENT
                  <IconButton onClick={() => console.log("drop down with info + log in")}>
                    <AccountCircleIcon sx={{width: 40, height: 40, color: "#fff"}}/>
                  </IconButton>
                </Typography>
              </Box>
            </Card>
            <Box display={"flex"} height={"100%"}>
              <Card sx={{
                boxShadow: 0,
                borderColor: "transparent",
                backgroundColor: "transparent",
                width: "28%",
                height: "90%"
              }}>
                <Card sx={{backgroundColor: "#fff", margin: 2, borderRadius: 2}}>
                  <List sx={{ overflow: 'auto', maxHeight: "75vh" }}>
                    <ListItem sx={{width: "100%", height: "100%"}}>
                      <Typography variant="h5" fontWeight={"bold"} color={"#697aa1"}>
                        Recent Measurements
                      </Typography>
                    </ListItem>
                    <ListItem disablePadding={true} sx={{borderColor: "red", borderWidth: 0.1}}>
                      <ListItemButton>
                        <ListItemText>
                          <Box display="flex" alignItems="center">
                            <CheckCircleIcon sx={{width: 40, height: 40, color: "green", marginLeft: 1}}/>
                            <Typography variant="body1" fontWeight={"bold"} paddingLeft={2}>
                              2024 - 27/01 Kl. 20:21
                            </Typography>
                          </Box>
                        </ListItemText>
                      </ListItemButton>
                    </ListItem>
                    <ListItem disablePadding={true}>
                      <ListItemButton>
                        <ListItemText>
                          <Box display="flex" alignItems="center">
                            <CancelIcon sx={{width: 40, height: 40, color: "red", marginLeft: 1}}/>
                            <Typography variant="body1" fontWeight={"bold"} paddingLeft={2}>
                              2022 - 12/09 Kl. 23:21
                            </Typography>
                          </Box>
                        </ListItemText>
                      </ListItemButton>
                    </ListItem>
                  </List>
                </Card>
              </Card>
              <Card sx={{
                boxShadow: 0,
                backgroundColor: "transparent",
                width: "72%",
                height: "85vh" }}>
                <Card sx={{backgroundColor: "white", width: "95%", height: "45%", marginLeft: 2, marginTop: 2}}>
                  <Typography sx={{margin: 2}} variant={"h4"}>Measurement (DATE OF MEASUREMENT)  </Typography>
                  
                </Card>
                <Card sx={{backgroundColor: "white", width: "95%", height: "40%", marginLeft: 2, marginTop: 2}}></Card>
              </Card>
            </Box>
          </Card>
        </Box>
      </>

  )
}

export default Home