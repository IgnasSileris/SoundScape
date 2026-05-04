import { contextBridge, ipcRenderer } from 'electron'

contextBridge.exposeInMainWorld('soundScapeApi', {
  getInputMics: () => ipcRenderer.invoke('devices:get-input-mics'),
  getOutputMics: () => ipcRenderer.invoke('devices:get-output-mics')
})
