import { UserButton, useUser } from "@clerk/clerk-react"
import '../../styles/Profile/UserPage.css'

function UserPage() {
    const { isSignedIn, user } = useUser();
    if(!isSignedIn) {
        return null;
    }
    return (
        <section>
            <h1>Salam <span id="user">Şükür</span> 👋{/* user.firstName */}</h1>
            <UserButton />
        </section>
        
    )
}

export default UserPage