import React, {useEffect, useState} from "react";
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
} from "@mui/material";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import AccessTimeFilledIcon from '@mui/icons-material/AccessTimeFilled';
import PatientLogo from "../assets/Logo.png";
import {Measurement} from "../../core/models/Measurement.ts";
import {MeasurementService} from "../../core/services/MeasurementService.ts";
import {useParams} from "react-router-dom";

export const Home: React.FunctionComponent = () => {
  const [measurements, setMeasurements] = useState<Measurement[]>([]);

  const {ssn} = useParams<{ ssn: string | undefined }>();

  const measurementService = new MeasurementService();

  const fetchMeasurements = async () => {
    try {
      const measurements = await measurementService.getMeasurementsBySSN(ssn);

      // Sort the inputs based on the updatedAt property in descending order
      measurements.sort((a: Measurement, b: Measurement) => b.id - a.id);

      setMeasurements(measurements);
    } catch (error) {
      console.error("Error fetching moderation inputs", error);
    }
  }

  useEffect(() => {
    fetchMeasurements();
  }, []);

  return (
      <>
        <CssBaseline/>
        <Box className="container" sx={{backgroundColor: "#717373"}}>
          <Card sx={{width: "80vw", height: "90vh", backgroundColor: "#4f5050"}}>
            <Card sx={{backgroundColor: "#626569", width: "100%", padding: 1}}>
              <Box display="flex" justifyContent="space-between">
                <Typography paddingLeft={2} variant="h5" color={"#343a40"}>
                  <img src={PatientLogo} alt={"patientUI"} style={{paddingTop: 4}} width={40}/>
                </Typography>
                <Typography variant="h6" sx={{color: "#cecaca"}}>
                  Marcus Iversen
                  <IconButton onClick={() => console.log("drop down with info + log in")}>
                    <AccountCircleIcon sx={{width: 40, height: 40, color: "rgba(34,34,241,0.66)"}}/>
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
                <Card sx={{backgroundColor: "#adb5bd", margin: 2, borderRadius: 2}}>
                  <List sx={{overflow: "auto", minHeight: "75vh", maxHeight: "75vh"}}>
                    <ListItem sx={{width: "100%", height: "100%"}}>
                      <Typography variant="h5" fontWeight={"bold"} color={"#212529"}>
                        Recent Measurements
                      </Typography>
                    </ListItem>
                    {measurements.map((measurement, index) => (
                        <ListItemButton key={measurement.id || index} sx={{borderRadius: 2}} onClick={() => {
                          console.log(measurement.viewedByDoctor)
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
                <Card sx={{backgroundColor: "#adb5bd", width: "95%", height: "45%", marginLeft: 2, marginTop: 2}}>
                  <Typography sx={{margin: 2, color: "#212529"}} variant={"h5"} fontWeight={"bold"}>
                    2024 - 25/4 kl. 12:33
                  </Typography>
                </Card>
                <Card
                    sx={{backgroundColor: "#adb5bd", width: "95%", height: "40%", marginLeft: 2, marginTop: 2}}></Card>
              </Card>
            </Box>
          </Card>
        </Box>
      </>
  );
};

export default Home;
