import {Box, Grid, Paper,} from '@mui/material'
import './Home.css'
import * as React from "react";

export const Home: React.FunctionComponent = () => {

  return (
      <Box className="container" sx={{backgroundColor: "#353635"}}>
        <Grid sx={{width: '60vw', height: '90vh', backgroundColor: "#353635"}}>
          <Paper></Paper>
        </Grid>
      </Box>
  )
}

export default Home