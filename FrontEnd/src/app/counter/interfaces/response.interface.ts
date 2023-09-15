import { Counter } from "./counter.interface";

export interface EndpointResponse {
  message: string;
  result:  Counter;
  error:   null;
}

