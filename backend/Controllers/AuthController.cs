using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeddingDressCMS.API.Models.Auth;
using WeddingDressCMS.API.Services;

namespace WeddingDressCMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>Authentication response with JWT token</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            Console.WriteLine($"🔐 Login attempt for: {request?.Email}");
            Console.WriteLine($"🔐 Headers: {string.Join(", ", Request.Headers.Select(h => $"{h.Key}={h.Value}"))}");
            
            try
            {
                if (!ModelState.IsValid)
                {
                    Console.WriteLine($"🔐 Invalid model state: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
                    return BadRequest(ModelState);
                }

                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login.", error = ex.Message });
            }
        }

        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="request">Registration details</param>
        /// <returns>Authentication response with JWT token</returns>
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _authService.RegisterAsync(request);
                return CreatedAtAction(nameof(GetUserInfo), new { }, response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during registration.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get current user information
        /// </summary>
        /// <returns>User information</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserInfo>> GetUserInfo()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "User not found in token." });
                }

                var userInfo = await _authService.GetUserInfoAsync(userId);
                return Ok(userInfo);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching user information.", error = ex.Message });
            }
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="request">Password change request</param>
        /// <returns>Success status</returns>
        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "User not found in token." });
                }

                var result = await _authService.ChangePasswordAsync(userId, request);
                if (result)
                {
                    return Ok(new { message = "Password changed successfully." });
                }

                return BadRequest(new { message = "Failed to change password. Please check your current password." });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while changing password.", error = ex.Message });
            }
        }

        /// <summary>
        /// User logout
        /// </summary>
        /// <returns>Success status</returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "User not found in token." });
                }

                await _authService.LogoutAsync(userId);
                return Ok(new { message = "Logged out successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during logout.", error = ex.Message });
            }
        }

        /// <summary>
        /// Check if the user is authenticated
        /// </summary>
        /// <returns>Authentication status</returns>
        [HttpGet("check")]
        [Authorize]
        public ActionResult CheckAuth()
        {
            return Ok(new { 
                message = "User is authenticated", 
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                email = User.FindFirstValue(ClaimTypes.Email)
            });
        }
    }
} 