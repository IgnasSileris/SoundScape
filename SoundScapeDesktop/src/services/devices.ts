import type { MicDeviceOption } from '../types'

export const fetchInputMics = async (): Promise<MicDeviceOption[]> => {
  return window.soundScapeApi.getInputMics()
}

export const fetchOutputMics = async (): Promise<MicDeviceOption[]> => {
  return window.soundScapeApi.getOutputMics()
}
