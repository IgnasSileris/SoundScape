import type { MicDeviceOption } from './index'

declare global {
  interface Window {
    soundScapeApi: {
      getInputMics: () => Promise<MicDeviceOption[]>
      getOutputMics: () => Promise<MicDeviceOption[]>
    }
  }
}

export {}
