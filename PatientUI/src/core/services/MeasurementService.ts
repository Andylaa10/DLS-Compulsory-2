import {CreateMeasurementDTO} from "../dtos/CreateMeasurementDTO.ts";
import axios from "axios";

export class MeasurementService {
  api = axios.create({
    baseURL: 'http://localhost:9090/api'
  })

  async getMeasurementsBySSN(ssn: string | undefined) {
    try {
      const response = await this.api.get(`/measurement/${ssn}`);
      return response.data;
    } catch (error) {
      console.error("Error in getMeasurementById: ", error);
      throw error;
    }
  }

  async createMeasurement(measurement: CreateMeasurementDTO) {
    try {
      const response = await this.api.post('/CreateMeasurement', measurement);
      return response.data;
    } catch (error) {
      console.error("Error in createMeasurement: ", error);
      throw error;
    }
  }

  async deleteMeasurement(id: number) {
    try {
      const response = await this.api.delete(`/DeleteMeasurement/${id}`);
      return response.data;
    } catch (error) {
      console.error("Error in deleteMeasurement: ", error);
      throw error;
    }
  }

}