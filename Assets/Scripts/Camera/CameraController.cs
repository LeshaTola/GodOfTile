using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float focusStep;
	[SerializeField] private float focusSpeed;

	[SerializeField] private CinemachineVirtualCamera virtualCamera;

	[Header("Constraints")]
	[SerializeField] private int maxXCoordinate = 10;
	[SerializeField] private int minXCoordinate = -10;
	[SerializeField] private int maxYCoordinate = 10;
	[SerializeField] private int minYCoordinate = -10;

	[SerializeField] private int maxOffset = 9;
	[SerializeField] private int minOffset = 4;

	private CinemachineTransposer cinemachineTransposer;

	private void Awake()
	{
		cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
	}

	private void Update()
	{
		Move(GameInput.Instance.GetMoveVectorNormalized());
		Focus();
	}

	private void Move(Vector2 dir)
	{
		var moveDir = transform.forward * dir.y + transform.right * dir.x;

		transform.position += moveDir * Time.deltaTime * moveSpeed;

		var x = Mathf.Clamp(transform.position.x, minXCoordinate, maxXCoordinate);
		var y = Mathf.Clamp(transform.position.z, minYCoordinate, maxYCoordinate);

		transform.position = new Vector3(x, transform.position.y, y);
	}

	private void Focus()
	{
		Vector3 targetFocus = cinemachineTransposer.m_FollowOffset;
		if (Mouse.current.scroll.ReadValue().y < 0)
		{
			targetFocus.y += focusStep;
		}

		if (Mouse.current.scroll.ReadValue().y > 0)
		{
			targetFocus.y -= focusStep;
		}

		targetFocus.y = Mathf.Clamp(targetFocus.y, minOffset, maxOffset);

		cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFocus, Time.deltaTime * focusSpeed);
	}

}
