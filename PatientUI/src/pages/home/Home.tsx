import React, {useEffect, useState} from "react";
import {
  Box,
  Button,
  Card,
  CssBaseline,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemText, Menu, MenuItem,
  TextField,
  Typography,
} from "@mui/material";

import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import LoginIcon from '@mui/icons-material/Login';import AccessTimeFilledIcon from '@mui/icons-material/AccessTimeFilled';
import PatientLogo from "../assets/Logo.png";
import PressureChart from "../assets/BloodPressureChart.png";
import {Measurement} from "../../core/models/Measurement.ts";
import {MeasurementService} from "../../core/services/MeasurementService.ts";
import {useParams} from "react-router-dom";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import {DatePicker, LocalizationProvider, TimePicker} from "@mui/x-date-pickers";
import {CreateMeasurementDTO} from "../../core/dtos/CreateMeasurementDTO.ts";
import {PatientService} from "../../core/services/PatientService.ts";


export const Home: React.FunctionComponent = () => {
  const [measurements, setMeasurements] = useState<Measurement[]>([]);
  const [selectedMeasurement, setSelectedMeasurement] = useState<Measurement | null>(null);
  const [systolic, setSystolic] = useState('');
  const [diastolic, setDiastolic] = useState('');
  const [selectedDate, setSelectedDate] = useState('');
  const [selectedTime, setSelectedTime] = useState('');
  const [patientName, setPatientName] = useState('');
  const [anchorEl, setAnchorEl] = useState(null);


  const {ssn} = useParams<{ ssn: string | undefined }>();


  const measurementService = new MeasurementService();
  const patientService = new PatientService();

  const fetchMeasurements = async () => {
    try {
      const measurements = await measurementService.getMeasurementsBySSN(ssn);

      // Sort the inputs based on the updatedAt property in descending order
      measurements.sort((a: Measurement, b: Measurement) => b.id - a.id);

      setSelectedMeasurement(measurements[0])
      setMeasurements(measurements);
    } catch (error) {
      console.error("Error fetching moderation inputs", error);
    }
  }

  const fetchPatientInfo = async () => {
    if (ssn === undefined) return;

    try {
      const patient = await patientService.getPatientBySsn(ssn);
      setPatientName(patient.name);
    } catch (error) {
      console.error("Error fetching moderation inputs", error);
    }
  }
  const getMeasurement = async (id: number) => {
    try {
      const measurement = await measurementService.getMeasurementById(id);
      setSelectedMeasurement(measurement);
      return measurement;
    } catch (error) {
      console.error("Error fetching moderation inputs", error);
    }
  }

  const createMeasurement = async (systolic: number, diastolic: number, selectedDate: string, selectedTime: string) => {

    try {
      const measurement: CreateMeasurementDTO = {
        systolic: systolic,
        diastolic: diastolic,
        date: selectedDate + 'T' + selectedTime + ":00",
        ssn: ssn
      };
      await measurementService.createMeasurement(measurement);
    } catch (error) {
      console.error("Error in handleCreateMeasurement: ", error);
    }
  };

  function handleMenu(event: any) {
    if (anchorEl !== event.currentTarget) {
      setAnchorEl(event.currentTarget);
    }
  }

  function handleClose() {
    setAnchorEl(null);
  }
  function logout() {
    location.href = "/login";
  }

  useEffect(() => {
    fetchPatientInfo();
    fetchMeasurements();
  }, []);

  return (
      <>
        <CssBaseline/>
        <Box className="container" sx={{backgroundColor: "#cbc7c7"}}>
          <Card sx={{width: "80vw", height: "90vh", backgroundColor: "#4f5050"}}>
            <Card sx={{backgroundColor: "#626569", width: "100%", padding: 1}}>
              <Box display="flex" justifyContent="space-between">
                <Typography paddingLeft={2} variant="h5" color={"#343a40"}>
                  <img src={PatientLogo} alt={"patientUI"} style={{paddingTop: 4}} width={40}/>
                </Typography>
                <Typography variant="h6" sx={{color: "#cecaca"}}>
                  {patientName}
                  <IconButton aria-owns={anchorEl ? "simple-menu" : undefined}
                              aria-haspopup="true" onMouseOver={handleMenu} onClick={handleMenu}>
                    <AccountCircleIcon sx={{width: 40, height: 40, color: "rgba(34,34,241,0.66)"}}/>
                  </IconButton>
                </Typography>
                <Menu
                    id="simple-menu"
                    anchorEl={anchorEl}
                    open={Boolean(anchorEl)}
                    onClose={handleClose}
                    MenuListProps={{onMouseLeave: handleClose}}
                >
                  <MenuItem onClick={logout}>Logout <LoginIcon sx={{marginLeft: 2}}/></MenuItem>
                </Menu>
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
                <Card sx={{backgroundColor: "#adb5bd", margin: 2, borderRadius: 2}}>
                  <List sx={{overflow: "auto", minHeight: "75vh", maxHeight: "75vh"}}>
                    <ListItem sx={{width: "100%", height: "100%"}}>
                      <Typography variant="h5" fontWeight={"bold"} color={"#212529"}>
                        Recent Measurements
                      </Typography>
                    </ListItem>
                    {measurements.map((measurement, index) => (
                        <ListItemButton key={measurement.id || index} sx={{borderRadius: 2}} onClick={() => {
                          getMeasurement(measurement.id);
                        }}>
                          <ListItemText sx={{marginLeft: 1}}
                                        primary={"Blood pressure"}
                                        secondary={measurement.date.toString()}>
                          </ListItemText>
                          {measurement.viewedByDoctor ?
                              <CheckCircleIcon sx={{marginTop: 0.5, width: 35, height: 35, color: "#528134"}}/> :
                              <AccessTimeFilledIcon sx={{marginTop: 0.5, width: 35, height: 35, color: "#d7832f"}}/>}

                        </ListItemButton>
                    ))}
                  </List>
                </Card>
              </Card>
              <Card sx={{
                boxShadow: 0,
                backgroundColor: "transparent",
                minWidth: "72%",
                height: "85vh"
              }}>
                <Card sx={{
                  padding: 2,
                  backgroundColor: "#adb5bd",
                  width: "95%",
                  height: "45%",
                  marginLeft: 2,
                  marginTop: 2
                }}>
                  <Typography sx={{color: "#212529"}} variant={"h5"} fontWeight={"bold"}>
                    Blood pressure - {selectedMeasurement?.date.toString()}
                  </Typography>
                  <Box display={"flex"} flexDirection={"row"} justifyContent={"space-between"}>
                    <Box width={"30%"} display={"flex"} flexDirection={"column"}>
                      <Typography variant="h6" fontWeight={"bold"} color={"#2f2fd3"} sx={{paddingTop: 2}}>
                        Systolic pressure:
                      </Typography>
                      <Typography variant="h6" fontWeight={"bolder"} color={"#000000"} sx={{paddingTop: 2}}>
                        {selectedMeasurement?.systolic}
                      </Typography>
                      <Typography variant="h6" fontWeight={"bold"} color={"#2f2fd3"} sx={{paddingTop: 2}}>
                        Diastolic pressure:
                      </Typography>
                      <Typography variant="h6" fontWeight={"bolder"} color={"#000000"} sx={{paddingTop: 2}}>
                        {selectedMeasurement?.diastolic}
                      </Typography>
                      <Typography sx={{paddingBottom: 2}}/>
                      <Typography variant={"subtitle2"}>
                        {selectedMeasurement?.viewedByDoctor ?
                            "- Reviewed by doctor" :
                            "- Not reviewed by doctor"}
                      </Typography>
                    </Box>
                    <Box width={"70%"} display={"flex"} flexDirection={"column"} justifyContent={"flex-end"}
                         alignItems={"flex-end"}>
                      <img src={PressureChart} alt={"blood pressure chart"} style={{maxWidth: 450}}/>
                    </Box>
                  </Box>
                </Card>
                <Card
                    sx={{backgroundColor: "#adb5bd", width: "95%", height: "40%", marginLeft: 2, marginTop: 2}}>
                  <Card sx={{
                    padding: 2,
                    boxShadow: 0,
                    backgroundColor: "transparent",
                    width: "95%",
                  }}>
                    <Typography variant={"h5"} fontWeight={"bolder"}>Blood Pressure - Create New
                      Measurement</Typography>
                    <Box display={"flex"} flexDirection={"row"} sx={{height: "100%"}}>
                      <Box display={"flex"} flexDirection={"column"} sx={{marginTop: 2}}>
                        <Typography variant={"subtitle1"} fontWeight={"bold"} color={"#2f2fd3"}>Systolic
                          pressure:</Typography>
                        <TextField
                            id="systolic"
                            placeholder={"Ex: 120"}
                            variant="outlined"
                            onBlur={(e) => setSystolic(e.target.value)}
                            sx={{marginBottom: 2}}/>
                        <Typography variant={"subtitle1"} fontWeight={"bold"} color={"#2f2fd3"}>Diastolic
                          pressure:</Typography>
                        <TextField
                            id="diastolic"
                            placeholder={"Ex: 80"}
                            variant="outlined"
                            onBlur={(e) => setDiastolic(e.target.value)}
                        />
                      </Box>
                      <Box display={"flex"} flexDirection={"column"} sx={{marginTop: 2, marginLeft: 4}}>
                        <Typography variant={"subtitle1"} fontWeight={"bold"} color={"#2f2fd3"}>Date of
                          measurement: </Typography>
                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                          <DatePicker

                              sx={{marginBottom: 2}}
                              onChange={(newValue) => {
                                if (newValue) {
                                  setSelectedDate(newValue.format('YYYY-MM-DD'))
                                }
                              }}
                          />
                        </LocalizationProvider>
                        <Typography variant={"subtitle1"} fontWeight={"bold"} color={"#2f2fd3"}>Date of
                          measurement: </Typography>
                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                          <TimePicker sx={{marginBottom: 2}} onChange={(newValue) => {
                            if (newValue) {
                              setSelectedTime(newValue.format('HH:mm'))
                            } else {
                              setSelectedTime(Date.now().toString())
                            }
                          }}/>
                        </LocalizationProvider>
                      </Box>
                      <Box display={"flex"} flexDirection={"row"} alignItems={"flex-end"} justifyContent={"flex-end"}
                           sx={{marginTop: 2, marginLeft: 4, marginBottom: 2.5}}>
                        <Button variant="contained" sx={{backgroundColor: "#2f2fd3", width: 250}} onClick={() => {
                          createMeasurement(parseInt(systolic), parseInt(diastolic), selectedDate, selectedTime);
                          window.location.reload();
                        }}>Create Measurement</Button>
                      </Box>
                    </Box>
                  </Card>
                </Card>
              </Card>
            </Box>
          </Card>
        </Box>
      </>
  );
};

export default Home;
