import { useQuery } from '@tanstack/react-query'
import { fetchInputMics, fetchOutputMics } from '../services/devices'

export const useInputMics = () => {
  return useQuery({
    queryKey: ['input-mics'],
    queryFn: fetchInputMics
  })
}

export const useOutputMics = () => {
  return useQuery({
    queryKey: ['output-mics'],
    queryFn: fetchOutputMics
  })
}
