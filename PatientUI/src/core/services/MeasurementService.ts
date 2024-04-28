import {CreateMeasurementDTO} from "../dtos/CreateMeasurementDTO.ts";
import axios from "axios";

export class MeasurementService {
  api = axios.create({
    baseURL: 'http://localhost:5206/api'
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

  async getMeasurementById(id: number) {
    try {
      const response = await this.api.get(`/measurement/GetMeasurement/${id}`);
      return response.data;
    } catch (error) {
      console.error("Error in getMeasurementById: ", error);
      throw error;
    }
  }

  async createMeasurement(measurement: CreateMeasurementDTO) {
    try {
      const response = await this.api.post('/measurement/CreateMeasurement', measurement);
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