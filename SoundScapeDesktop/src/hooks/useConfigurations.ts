import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import {
  deleteMicConfig,
  fetchMicConfigs,
  saveMicConfig
} from '../services/configurations'

export const useMicConfigs = () => {
  return useQuery({
    queryKey: ['saved-mic-configs'],
    queryFn: fetchMicConfigs
  })
}
// TODO: False should go into error
export const useSaveMicConfig = () => {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: saveMicConfig,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['saved-mic-configs'] })
    }
  })
}

export const useDeleteMicConfig = () => {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: deleteMicConfig,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['saved-mic-configs'] })
    }
  })
}
