import '../styles/Navbar.css'

function Navbar() {

  return (
    <>
      <nav>
        <a href="/" id='logo'><img src="/assets/bookLogo.png" alt="logo"/></a>
        <div>
          <ul>
            <li>
              <a href="/">Ana səhifə</a>
            </li>
            <li>
              <a href="/login">Daxil ol</a>
            </li>
            <li>
              <a href="/about">Haqqımızda</a>
            </li>
            <li>
              <a href="/contact">Əlaqə</a>
            </li>
          </ul>
        </div>
      </nav>
    </>
  )
}

export default Navbar
