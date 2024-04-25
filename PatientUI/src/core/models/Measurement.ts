import moment from "moment";

export interface Measurement{
  id: number;
  date: moment.Moment;
  systolic: number;
  diastolic: number;
  ssn: string;
  viewedByDoctor: boolean;
}