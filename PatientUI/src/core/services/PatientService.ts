import axios from 'axios'

export class PatientService{
  api = axios.create({
    baseURL: 'http://localhost:9092/api'
  })

  async getPatientBySsn(ssn: string) {
    try {
      const response = await this.api.get(`/patients/${ssn}`);
      return response.data;
    } catch (error) {
      console.error("Error in getPatientBySsn: ", error);
      throw error;
    }
  }


}