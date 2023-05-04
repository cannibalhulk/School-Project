import '../styles/Navbar.css'

function Navbar() {

  return (
    <>
      <nav>
        <a href="/">Loqo</a>
        <div>
          <ul>
            <li>
              <a href="/">Ana səhifə</a>
            </li>
            <li>
              <a href="/daxilol">Daxil ol</a>
            </li>
            <li>
              <a href="/haqqinda">Haqqımızda</a>
            </li>
            <li>
              <a href="/elaqe">Əlaqə</a>
            </li>
          </ul>
        </div>
      </nav>
    </>
  )
}

export default Navbar
