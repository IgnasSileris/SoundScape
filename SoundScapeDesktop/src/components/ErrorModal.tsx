import { AlertCircle } from 'lucide-react'
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogHeader,
  DialogDescription,
  DialogFooter
} from './ui/dialog'
import { Button } from './ui/button'

type ErrorModalProps = {
  isOpen: boolean
  message: string
  onClose: () => void
}
const ErrorModal = ({ isOpen, message, onClose }: ErrorModalProps) => {
  return (
    <Dialog
      open={isOpen}
      onOpenChange={(isDialogOpen) => !isDialogOpen && onClose()}
    >
      <DialogContent className="border border-red-500/30 bg-slate-950 text-white shadow-2xl shadow-red-950/30 sm:max-w-md">
        <DialogHeader className="gap-4">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-full bg-red-500/10 text-red-400">
              <AlertCircle className="size-5" />
            </span>
            <DialogTitle className="text-xl font-medium text-white">
              Something went wrong
            </DialogTitle>
          </div>
          <DialogDescription className="text-base leading-7 text-slate-300">
            {message}
          </DialogDescription>
        </DialogHeader>
        <DialogFooter>
          <Button
            onClick={onClose}
            className="rounded-full bg-sky-500 px-6 text-slate-950 hover:bg-sky-400"
          >
            OK
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}

export default ErrorModal
