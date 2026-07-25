import { contextBridge, ipcRenderer } from 'electron'

// we keep these untyped to avoid overcomplicating import paths, the paylooads are properly typed at the src level.

contextBridge.exposeInMainWorld('soundScapeApi', {
  getInputMics: () => ipcRenderer.invoke('devices:get-input-mics'),
  getOutputMics: () => ipcRenderer.invoke('devices:get-output-mics'),

  getAllConfigs: () => ipcRenderer.invoke('config:get-all'),
  saveConfig: (payload: any) => ipcRenderer.invoke('config:save', payload),
  deleteConfig: (payload: any) => ipcRenderer.invoke('config:delete', payload),

  updateAudioState: (payload: any) =>
    ipcRenderer.invoke('audio-state:update', payload)
})
